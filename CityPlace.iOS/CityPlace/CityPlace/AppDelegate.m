//
//  AppDelegate.m
//  CityPlace
//
//  Created by Yuri Korshev on 15.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "AppDelegate.h"
#import "AFNetworking/AFNetworking.h"
#import "Flurry.h"

@interface AppDelegate ()

@end

@implementation AppDelegate


- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    
    // APNS registration
    [[UIApplication sharedApplication] registerForRemoteNotificationTypes: (UIRemoteNotificationTypeBadge | UIRemoteNotificationTypeSound | UIRemoteNotificationTypeAlert)];
    
    [Flurry startSession:@"M597GV74YJS255H5MFS7"];
    [[UIApplication sharedApplication] setApplicationIconBadgeNumber:0];
    
    return YES;
}

- (void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken  {
    NSLog(@"My token is: %@", deviceToken);
    NSString *token = [[[NSString stringWithFormat:@"%@",deviceToken] stringByTrimmingCharactersInSet:[NSCharacterSet characterSetWithCharactersInString:@"<>"]] stringByReplacingOccurrencesOfString:@" " withString:@""];
    NSLog(@"My processed token is: %@", token);
    
    // Регистрируем на сервере
    AFHTTPRequestOperationManager *manager = [AFHTTPRequestOperationManager manager];
    [[UIApplication sharedApplication] setNetworkActivityIndicatorVisible:YES];
    NSString *url = @"http://cityplace.softgears.ru/mobile-api/register-device";
    NSString *oldToken = [[NSUserDefaults standardUserDefaults] valueForKey:@"currentToken"];
    if (oldToken == nil){
        oldToken = @"";
    }
    NSInteger cityId = [self getCityId];
    NSDictionary *p = @{@"platform": [NSNumber numberWithInt:1], @"old": oldToken, @"token":token, @"cityId": [NSNumber numberWithInt:cityId]};
    [manager GET:url parameters:p success:^(AFHTTPRequestOperation *operation, id responseObject) {
        [[UIApplication sharedApplication] setNetworkActivityIndicatorVisible:NO];
        NSLog(@"Зарегистрировались на сервере: %@", responseObject);
        [[NSUserDefaults standardUserDefaults] setValue:token forKey:@"currentToken"];
        [[NSUserDefaults standardUserDefaults] synchronize];
    } failure:^(AFHTTPRequestOperation *operation, NSError *error) {
        [[UIApplication sharedApplication] setNetworkActivityIndicatorVisible:NO];
        NSLog(@"Error: %@", error);
    }];
}

- (void)applicationWillResignActive:(UIApplication *)application {
    // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
    // Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
}

- (void)applicationDidEnterBackground:(UIApplication *)application {
    // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later.
    // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
}

- (void)applicationWillEnterForeground:(UIApplication *)application {
    // Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
}

- (void)applicationDidBecomeActive:(UIApplication *)application {
    // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
}

- (void)applicationWillTerminate:(UIApplication *)application {
    // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
}

// Получает текущий идентификатор города
- (NSInteger) getCityId
{
    NSUserDefaults *defs = [NSUserDefaults standardUserDefaults];
    NSInteger cityId = [defs integerForKey:@"cityId"];
    if (cityId == 0){
        cityId = 1;
    }
    return cityId;
}

@end

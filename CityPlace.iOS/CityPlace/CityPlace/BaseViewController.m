//
//  BaseViewController.m
//  CityPlace
//
//  Created by Yuri Korshev on 16.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import "BaseViewController.h"
#import "SWRevealViewController.h"

@interface BaseViewController ()

@end

@implementation BaseViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    
    SWRevealViewController *revealViewController = self.revealViewController;
    if ( revealViewController )
    {
        [self.view addGestureRecognizer: self.revealViewController.panGestureRecognizer];
    }
    
    UIImageView *backgroundImage = [[UIImageView alloc] initWithImage:[UIImage imageNamed:@"bg"]];
    backgroundImage.contentMode = UIViewContentModeScaleAspectFit;
    [backgroundImage updateConstraints];
    
    [self.view addSubview:backgroundImage];
    [self.view sendSubviewToBack:backgroundImage];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

#pragma mark - Common functions

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

// Получает текущее наименование города
- (NSString*) getCityName
{
    NSUserDefaults *defs = [NSUserDefaults standardUserDefaults];
    NSString* cityName = [defs stringForKey:@"cityName"];
    if (cityName == nil){
        cityName = @"Хабаровск";
    }
    return cityName;
}

- (void) updateCurrentCityWithId:(NSNumber *)cityId andName:(NSString*)cityName {
    NSUserDefaults *defs = [NSUserDefaults standardUserDefaults];
    [defs setObject:cityId forKey:@"cityId"];
    [defs setObject:cityName forKey:@"cityName"];
    [defs synchronize];
    
    [[UIApplication sharedApplication] registerForRemoteNotificationTypes: (UIRemoteNotificationTypeBadge | UIRemoteNotificationTypeSound | UIRemoteNotificationTypeAlert)];
}

// Выполняет асинхронный GET запрос по указанному URL
- (void) getJsonFromUrl: (NSString *) url success: (void (^)(id))successCallback error: (void (^)(NSError *)) errorCallback
{
    AFHTTPRequestOperationManager *manager = [AFHTTPRequestOperationManager manager];
    [[UIApplication sharedApplication] setNetworkActivityIndicatorVisible:YES];
    [manager GET:url parameters:nil success:^(AFHTTPRequestOperation *operation, id responseObject) {
        [[UIApplication sharedApplication] setNetworkActivityIndicatorVisible:NO];
        successCallback(responseObject);
        //NSLog(@"JSON: %@", responseObject);
    } failure:^(AFHTTPRequestOperation *operation, NSError *error) {
        [[UIApplication sharedApplication] setNetworkActivityIndicatorVisible:NO];
        if (error != nil){
            errorCallback(error);
        }
        NSLog(@"Error: %@", error);
    }];
}

@end

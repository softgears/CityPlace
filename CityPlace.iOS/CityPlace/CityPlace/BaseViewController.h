//
//  BaseViewController.h
//  CityPlace
//
//  Created by Yuri Korshev on 16.10.14.
//  Copyright (c) 2014 SoftGears. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "AFNetworking.h"

IB_DESIGNABLE
@interface BaseViewController : UIViewController

- (NSInteger) getCityId;
- (NSString *) getCityName;
- (void) updateCurrentCityWithId:(NSNumber *)cityId andName:(NSString*)cityName;
- (void) getJsonFromUrl: (NSString *) url success: (void (^)(id))successCallback error: (void (^)(NSError *)) errorCallback;

@end
